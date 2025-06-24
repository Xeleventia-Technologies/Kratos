export type LoginRequest = {
  email: string;
  password: string;
};

export type LoginResponse = {
  accessToken: string;
  id: number;
  email: string;
  name: string;
  displayPictureFileName?: string;
};
