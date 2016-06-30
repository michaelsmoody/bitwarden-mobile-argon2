﻿using System;
using SQLite;
using Bit.App.Abstractions;
using Bit.App.Models.Api;

namespace Bit.App.Models.Data
{
    [Table("Folder")]
    public class FolderData : IDataObject<string>
    {
        public FolderData()
        { }

        public FolderData(Folder folder, string userId)
        {
            Id = folder.Id;
            UserId = userId;
            Name = folder.Name?.EncryptedString;
        }

        public FolderData(FolderResponse folder, string userId)
        {
            Id = folder.Id;
            UserId = userId;
            Name = folder.Name;
        }

        public FolderData(CipherResponse cipher, string userId)
        {
            if(cipher.Type != Enums.CipherType.Folder)
            {
                throw new ArgumentException(nameof(cipher.Type));
            }

            var data = cipher.Data as FolderDataModel;
            if(data == null)
            {
                throw new ArgumentException(nameof(cipher.Data));
            }

            Id = cipher.Id;
            UserId = userId;
            Name = data.Name;
        }

        [PrimaryKey]
        public string Id { get; set; }
        [Indexed]
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime RevisionDateTime { get; set; } = DateTime.UtcNow;

        public Folder ToFolder()
        {
            return new Folder(this);
        }
    }
}
