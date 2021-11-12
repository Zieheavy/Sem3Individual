
import React, { useState } from 'react';
import KeyboardEventHandler from 'react-keyboard-event-handler';

const PongInput = (props) => {
    const [id, setId] = useState('');
    const [p1Pos, setP1Pos] = useState('');
    const [p2Pos, setP2Pos] = useState('');
    const [balX, setBalX] = useState('');
    const [balY, setBalY] = useState('');
    // const [pos] = useState(0);

    const onSubmit = (e) => {
        e.preventDefault();

        const isIdProvided = id && id !== '';
        const isP1PosProvided = p1Pos && p1Pos !== '';
        const isP2PosProvided= p2Pos && p2Pos !== '';
        const isBalXProvided = balX && balX !== '';
        const isBalyProvided = balY && balY !== '';

        if (isIdProvided && isP1PosProvided && isP2PosProvided && isBalXProvided && isBalyProvided) {
            props.sendMessage(id, p1Pos, p2Pos, balX, balY);
            console.log("SendMessage");
        } 
        else {
            alert('Please insert info');
        }
    }


    const ComponentA = (props) => (<div>
    <div>key detected: {props.eventKey}</div>
    <KeyboardEventHandler
        handleKeys={['a', 'b', 'c']}
        onKeyEvent={(key, e) => console.log(`do something upon keydown event of ${key}`)} />
    </div>);

    const onIdUpdate = (e) => {
        setId(e.target.value);
    }
    const onP1PosUpdate = (e) => {
        setP1Pos(e.target.value);
    }
    const onP2PosUpdate = (e) => {
        setP2Pos(e.target.value);
    }
    const onBalXUpdate = (e) => {
        setBalX(e.target.value);
    }
    const onBalYUpdate = (e) => {
        setBalY(e.target.value);
    }

    return (
        <div>
        <form 
            onSubmit={onSubmit}>
            <label htmlFor="id">Id:</label>
            <br />
            <input 
                id="id" 
                name="id" 
                value={id}
                onChange={onIdUpdate} />
            <br/>
            
            <label htmlFor="p1Pos">p1Pos:</label>
            <br />
            <input 
                id="p1Pos" 
                name="p1Pos" 
                value={p1Pos}
                onChange={onP1PosUpdate} />
            <br/>
            
            <label htmlFor="p2Pos">p2Pos:</label>
            <br />
            <input 
                id="p2Pos" 
                name="p2Pos" 
                value={p2Pos}
                onChange={onP2PosUpdate} />
            <br/>
            
            <label htmlFor="balXPos">balXPos:</label>
            <br />
            <input 
                id="balXPos" 
                name="balXPos" 
                value={balX}
                onChange={onBalXUpdate} />
            <br/>
            
            <label htmlFor="balyPos">balYPos:</label>
            <br />
            <input 
                id="balyPos" 
                name="balyPos" 
                value={balY}
                onChange={onBalYUpdate} />
            <br/>

            <br/>
            <button>Submit</button>
        </form>
        </div>
    )
};

export default PongInput;