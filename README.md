# CartonCaps Invitation API
This was developed in a development container and thus runs best there.

This assumes that you have the "Remove Development" pack extension installed in VS Code (Extension ID: ms-vscode-remote.vscode-remote-extensionpack)

## Build the Dev Container

After cloning this repository, and opening this folder in VS Code you will likely be prompted to Build and Open the Dev Container.

If you do not, then open the command palette (CTRL+SHIFT+P, or View -> Command Palette from the menus) and find the "Dev Containers: Rebuild and Reopen in Container" command. 

After it has downloaded, and you are in a dev container, open a new terminal within vscode using CTRL+SHIFT+` or Terminal -> New Terminal from the menus.

## Build and unit test the project

It should take you to a ~/cartoncaps prompt

Execute:

```bash
./start.sh
```

This will:
- Restore entity framework tools
- Restore the project
- Build the project
- Run the unit tests for the project
- Start the web app

Once the web app has been created go to http://localhost:5174/swagger/index.html

## Happy Path
- Use the POST /accounts path to create a random account with any email address that you like
- Use the POST /invitations path to create an invitation
- Use the POST /accounts path to create the account for the invited user
- Use the POST /invitations/{invitationID}/accept path to formally accept the invitation for the newly created user

Feel free to explore the other routes, and test unhappy path situations such as providing a blank email address, or an invalid referral code.

Sometimes initially the language service for C# in VS Code get's unhappy. If the project has been built sucesfully but VS Code is complaining about some thing then open the command pallet and execute the "Developer: Reload Window" command.
