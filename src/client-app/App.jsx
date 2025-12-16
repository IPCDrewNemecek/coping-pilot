import React, { useEffect, useState } from "react";
import ActivityList from "./components/ActivityList";

const App = () => {
    const [activities, setActivities] = useState([]);

    useEffect(() => {
        fetch("/api/activities")
            .then((response) => response.json())
            .then((data) => {
                const formattedActivities = Object.entries(data).map(([name, details]) => ({
                    name,
                    ...details,
                }));
                setActivities(formattedActivities);
            });
    }, []);

    return (
        <div style={{ padding: "2rem" }}>
            <h1>Mergington High School Activities</h1>
            <ActivityList activities={activities} />
        </div>
    );
};

export default App;