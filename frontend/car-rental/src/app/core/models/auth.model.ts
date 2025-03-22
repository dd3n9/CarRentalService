export interface AuthenticationResult {
  authenticationDto: AuthenticationDto;
  token: string;
}

export interface AuthenticationDto {
  userId: string;
  firstName: string;
  lastName: string;
  userRoles: string[];
}
