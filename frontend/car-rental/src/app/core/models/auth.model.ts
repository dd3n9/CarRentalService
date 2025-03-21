export interface AuthenticationResult {
  AuthenticationDto: AuthenticationDto;
  Token: string;
}

export interface AuthenticationDto {
  UserId: string;
  FirstName: string;
  LastName: string;
  UserRoles: string[];
}
