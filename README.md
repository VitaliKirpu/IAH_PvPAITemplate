<p align="center">  <img src="iah_logo.png" width="350" title="IAH: INTERNET WAR logo"/> </p>

IAH: INTERNET WAR is a futuristic strategy game you can play using a programming language or a computer mouse.

Participate in tournaments and stand a chance to win prizes that can change your life.

For programmers seeking a competitive challenge, IAH offers algorithmic multiplayer. Create or join competitive clubs, write code solo or collaborate in a group, and use your preferred IDE and programming language to wage highly competitive algorithmic wars.

<p align="left"><img src="GIF_1.gif" title="combat bot robots shooting"/> </p>

Steam Page

**https://store.steampowered.com/app/304770/IAH_INTERNET_WAR/**

Website

**https://iamhacker.cc/**


PvP API Documentation Wiki:

https://github.com/VitaliKirpu/IAH_PvPAITemplate/wiki/IAH:-INTERNET-WAR-%7C-PVP-AI-API
```mermaid
flowchart TD
    A[C# / JavaScript / C++ / Rust / Etc] --> B
    B[http://127.0.0.1:6800/request_name] --> C{GameClient.exe}

    C -->|Requires API Password|D[ v1/botaction]
    C -->|Requires API Access|G[ v1/apipassword]
    C -->F[ v1/entities]

G -->U[API Access is obtained from https://iamhacker.cc]
```
