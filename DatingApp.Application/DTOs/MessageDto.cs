﻿using DatingApp.Domain.Entities;

namespace DatingApp.Application.DTOs;

public class MessageDto
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public required string SenderUsername { get; set; }
    public required string SenderPhotoUrl { get; set; }
    public int RecipientId { get; set; }
    public required string RecipentUsername { get; set; }
    public required string RecipentPhotoUrl { get; set; }
    public required string Content { get; set; }
    public DateTime? DateRead { get; set; }
    public DateTime MessageSent { get; set; }

}
