import React from 'react';

import PongM from './PongM';

const PongWindow = (props) => {
    const chat = props.chat
        .map(m => <PongM 
            key={Date.now() * Math.random()}
            id={m.id}
            p1Pos={m.p1Pos}
            p2Pos={m.p1Pos}
            balX={m.balX}
            balY={m.balY}
            
            />);

    return(
        <div>
            {chat}
        </div>
    )
};

export default PongWindow;