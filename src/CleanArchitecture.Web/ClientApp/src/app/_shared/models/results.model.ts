export interface CreatedResult {
    id: string;
}

export interface Envelope{
    errorMessage: string;
    status: number;
    timestamp: Date;
    traceId: string;
}