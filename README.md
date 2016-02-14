# ImageUploader

This is a .NET 4 app used to upload files parsed as arguments to an FTP server.
This can be used in combination of Greenshot's external commands destination to quickly upload images.

### Installation

- Either build the solution or use files in Binary folder. 
- Copy config.example.ini to config.ini with your own values.

### Explanation of config.ini

- [FTP] Address: address of your ftp server.
- [FTP] Username: username for your ftp server (can be `username`, or `user@domain.tld`).
- [FTP] Password: password for your ftp server.
- [HTTP] Address: base URL for the destination, for example if your uploaded file is at `http://i.domain.tld/image.png`, the setting would be the `http://i.domain.tld/` part.