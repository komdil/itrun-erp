﻿namespace Contracts.Response.Auth
{
    public record AccountSignUpResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
