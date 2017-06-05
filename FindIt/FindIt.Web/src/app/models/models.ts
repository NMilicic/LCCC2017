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

export class Game{
    GameId: string;
    Questions:Question[]
    Username: string
}