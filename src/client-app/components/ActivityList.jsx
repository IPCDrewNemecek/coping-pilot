import React from "react";
import ActivityCard from "./ActivityCard";

const ActivityList = ({ activities }) => {
    return (
        <div style={{ display: "flex", flexWrap: "wrap", gap: "1rem" }}>
            {activities.map((activity, index) => (
                <ActivityCard key={index} activity={activity} />
            ))}
        </div>
    );
};

export default ActivityList;