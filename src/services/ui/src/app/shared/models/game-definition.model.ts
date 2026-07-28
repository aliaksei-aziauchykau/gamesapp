export type GameDefinition = {
  id: number;
  code: string;
  name: string;
  description: string | null;
  imageUrl: string | null;
  imageAlt: string | null;
  gamePageAppUrl: string | null;
};

export type GameSession = {
  id: number;
  gameDefinitionId: number;
  title: string;
  status: string;
  createdAt: string;
};

export type GameLobby = {
  id: number;
  gameDefinitionId: number;
  name: string;
  isOpen: boolean;
  createdAt: string;
};

export type ODataCollection<T> = { value: T[] };
