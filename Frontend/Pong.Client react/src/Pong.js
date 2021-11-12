import React, { useState, useEffect, useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';

import PongInput from './PongInput';
import PongWindow from './PongWindow';


const Pong = () => {
    const [ connection, setConnection ] = useState(null);
    const [ chat, setChat ] = useState([]);
    const latestChat = useRef(null);

    latestChat.current = chat;

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('http://localhost:5000/hubs/chat'
            )
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, []);

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(result => {
                    console.log('Connected!');
    
                    connection.on('ReceiveMessage', message => {
                        console.log("ReceiveMessage");
                        console.log(message);
                        const updatedChat = [...latestChat.current];
                        updatedChat.push(message);
                    
                        setChat(updatedChat);
                    });
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [connection]);

    const sendMessage = async (id, p1Pos, p2Pos, balX, balY) => {
        const chatMessage = {
            gameId: id,
            p1Pos: p1Pos,
            p2Pos: p2Pos,
            balX: balX,
            balY: balY
        };

        if (connection.connectionStarted) {
            try {
                console.log("MEssage Has been send");
                console.log(chatMessage);
                await connection.send('SendMessage', chatMessage);
            }
            catch(e) {
                console.log(e);
            }
        }
        else {
            alert('No connection to server yet.');
        }
    }

    return (
        <div>
            <PongInput sendMessage={sendMessage} />
            <hr />
            <PongWindow chat={chat}/>
        </div>
    );
};

export default Pong;