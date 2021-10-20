﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ChinookSystem.Entities
{
    internal partial class Playlist
    {
        public Playlist()
        {
            PlaylistTracks = new HashSet<PlaylistTrack>();
        }

        [Key]
        public int PlaylistId { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        [StringLength(120)]
        public string UserName { get; set; }

        [InverseProperty(nameof(PlaylistTrack.Playlist))]
        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }
    }
}