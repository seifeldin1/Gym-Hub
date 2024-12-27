Use GymHub;

CREATE TABLE
    IF NOT EXISTS ClientProgress (
        Client_Progress_ID INT AUTO_INCREMENT PRIMARY KEY,
        Client_ID INT,
        Coach_ID INT,
        ReportDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
        ProgressSummary VARCHAR(400) NOT NULL,
        GoalsAchieved VARCHAR(200) NOT NULL,
        ChallengesFaced VARCHAR(200) NOT NULL,
        NextSteps VARCHAR(300) NOT NULL,
        FOREIGN KEY (Client_ID) REFERENCES Client (Client_ID) ON DELETE CASCADE ON UPDATE CASCADE,
        FOREIGN KEY (Coach_ID) REFERENCES Coach (Coach_ID) ON DELETE SET NULL ON UPDATE CASCADE
    );