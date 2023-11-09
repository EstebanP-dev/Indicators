import { ActorType } from ".";

type ActorByIdResponse = {
    id: string;
    name: string;
    actorType: ActorType
}

export type {
    ActorByIdResponse,
}