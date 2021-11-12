import React from 'react';

const PongM = (props) => (
    <div style={{ background: "#eee", borderRadius: '5px', padding: '0 10px' }}>
        <p><strong>{props.id}</strong> says:</p>
        <p>{props.p1Pos}</p>
        <p>{props.p2Pos}</p>
        <p>{props.balX}</p>
        <p>{props.balY}</p>
    </div>
);

export default PongM;