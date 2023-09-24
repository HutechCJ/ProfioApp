import React from 'react';
import { HubConnectionBuilder, HttpTransportType } from '@microsoft/signalr';

function useSignalR(webSocketURI: string) {
  const [connection, setConnection] = React.useState(() =>
    new HubConnectionBuilder()
      .withUrl(webSocketURI, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
      })
      .build()
  );

  React.useEffect(() => {
    connection.start().then(() => {
      console.log(`INITIALIZED WEBSOCKET CONNECTION TO ${webSocketURI}`);
    });

    return () => {
      connection.stop();
    };
  }, []);

  return { connection, setConnection };
}

export default useSignalR;
