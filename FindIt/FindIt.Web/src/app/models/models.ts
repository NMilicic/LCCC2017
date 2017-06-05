export class Question {
    Name: string;
    ImageUri: string;
    Info: string;
    FirstHint: string;
    SecondHint: string;
    Latitude: number;
    Longitude: number;
    Answered: boolean;
    Score: number;
    Order: number;
    AnswerLatitude?: number;
    AnswerLongitude?: number;
    UsedFirstHint: boolean;
    UsedSecondHint: boolean;
}

export class Game {
    GameId: string;
    Questions: Question[]
    Username: string
}

export class EndGame {
    Score: number;
    NewAchievements: NewAchievements;

    EndGame(){
        this.Score= 0;
        this.NewAchievements = new NewAchievements();
    }
}

export class NewAchievements {
    Description: string;
    ImageUri: string;
    Name: string;
}