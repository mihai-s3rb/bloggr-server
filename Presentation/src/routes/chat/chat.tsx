import { useState, useRef, useEffect } from "react";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useAppSelector } from "../../features/hooks";
export const Chat = () => {
  const userData = useAppSelector((store) => store.user);
  const token = userData.token;
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [chat, setChat] = useState<string[]>([]);
  const latestChat = useRef<string[] | null>(null);

  const [inputValue, setInputValue] = useState("");
  latestChat.current = chat;

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:5080/hub", {
        accessTokenFactory: () => token!,
      })
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          connection.on("messageReceived", (username, message) => {
            if (latestChat.current != null) {
              const newChat = [...latestChat.current];
              newChat.push(`${username} says ${message}`);
              setChat(newChat);
            }
          });
        })
        .catch((err) => console.log("Err " + err));
    }
  }, [connection]);
  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setInputValue(e.target.value);
  };
  const sendMessage = async () => {
    if (connection) {
      // console.log("preparing to send");
      connection.invoke("SendMessageToUser", "marcel", inputValue);
      // .then(() => console.log("sent successfully"));
    }
  };

  return (
    <div>
      <div>
        {chat.map((message) => {
          return <p>{message}</p>;
        })}
      </div>
      <input
        value={inputValue}
        onChange={handleInputChange}
        type="text"
        placeholder="Write message"
      />
      <button onClick={sendMessage}>Send</button>
    </div>
  );
};
