import React from "react";
import { Card, CardHeader, CardBody, CardFooter } from "@progress/kendo-react-layout";

const ActivityCard = ({ activity }) => {
    return (
        <Card style={{ width: "300px", margin: "1rem" }}>
            <CardHeader>
                <h3>{activity.name}</h3>
            </CardHeader>
            <CardBody>
                <p><strong>Description:</strong> {activity.description}</p>
                <p><strong>Schedule:</strong> {activity.schedule}</p>
                <p><strong>Max Participants:</strong> {activity.maxParticipants}</p>
                <div>
                    <strong>Participants:</strong>
                    {activity.participants.length > 0 ? (
                        <ul style={{ paddingLeft: "1.5rem", marginTop: "0.5rem" }}>
                            {activity.participants.map((participant, index) => (
                                <li key={index} style={{ marginBottom: "0.25rem" }}>
                                    {participant}
                                </li>
                            ))}
                        </ul>
                    ) : (
                        <p style={{ fontStyle: "italic", color: "gray" }}>No participants yet.</p>
                    )}
                </div>
            </CardBody>
            <CardFooter>
                <button className="k-button k-primary">Sign Up</button>
            </CardFooter>
        </Card>
    );
};

export default ActivityCard;