import { ApolloProvider } from 'react-apollo';

import client from '../client';

export default function Index() {
    return (
        <ApolloProvider client={client}>
            <div>hello worlds</div>
        </ApolloProvider>
    );
}
