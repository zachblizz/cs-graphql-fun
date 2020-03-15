import isomorphicFetch from 'isomorphic-fetch';
import { HttpLink } from 'apollo-link-http';
import ApolloClient from 'apollo-client';
import { ApolloLink, split } from 'apollo-link';
import { onError } from 'apollo-link-error';
import { InMemoryCache } from 'apollo-cache-inmemory';
import { getMainDefinition } from 'apollo-utilities';

const httpLink = new HttpLink({
    uri: 'http://localhost:5000/graphql',
    fetch: (input: RequestInfo, init?: RequestInit) =>
        isomorphicFetch(input, init).then((res: Response) => {
            return res;
        }),
});

const networkLink = split(({ query }) => {
    const definition = getMainDefinition(query);
    return (
        definition.kind === 'OperationDefinition' &&
        definition.operation === 'subscription'
    );
}, httpLink);

const client = new ApolloClient({
    link: ApolloLink.from([
        onError(({ graphQLErrors, networkError }: any) => {
            if (graphQLErrors) {
                graphQLErrors.foreach(({ message, locations, path }: any) => {
                    console.log(
                        `[GraphQL error]: Message ${message}, Location: ${locations}, Path: ${path}`,
                    );
                });
            }

            if (networkError) console.log(`[Network error]: ${networkError}`);
        }),
        networkLink,
    ]),
    cache: new InMemoryCache(),
});

export default client;
