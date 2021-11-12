
import React, { useState } from 'react';
import KeyboardEventHandler from 'react-keyboard-event-handler';

const ChatInput = (props) => {
    const [user, setUser] = useState('');
    const [message, setMessage] = useState('');
    // const [pos] = useState(0);

    const onSubmit = (e) => {
        e.preventDefault();

        const isUserProvided = user && user !== '';
        const isMessageProvided = message && message !== '';

        if (isUserProvided && isMessageProvided) {
            props.sendMessage(user, message);
            console.log("SendMessage");
        } 
        else {
            alert('Please insert an user and a message.');
        }
    }


    const ComponentA = (props) => (<div>
    <div>key detected: {props.eventKey}</div>
    <KeyboardEventHandler
        handleKeys={['a', 'b', 'c']}
        onKeyEvent={(key, e) => console.log(`do something upon keydown event of ${key}`)} />
    </div>);

    const onUserUpdate = (e) => {
        setUser(e.target.value);
    }

    const onMessageUpdate = (e) => {
        setMessage(e.target.value);
    }

    return (
        <div>
        <form 
            onSubmit={onSubmit}>
            <label htmlFor="user">User:</label>
            <br />
            <input 
                id="user" 
                name="user" 
                value={user}
                onChange={onUserUpdate} />
            <br/>
            <label htmlFor="message">Message:</label>
            <br />
            <input 
                type="text"
                id="message"
                name="message" 
                value={message}
                onChange={onMessageUpdate} />
            <br/><br/>
            <button>Submit</button>
        </form>
        </div>
    )
};

export default ChatInput;