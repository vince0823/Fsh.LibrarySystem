namespace FSH.Learn.Application.Identity.Tokens;

public record RefreshTokenRequest(string Token, string RefreshToken);