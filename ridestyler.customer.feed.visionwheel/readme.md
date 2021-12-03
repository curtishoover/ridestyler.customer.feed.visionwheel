# ridestyler.customer.feed.konig
## Distributor feed for Konig


This project requires two things to be set up properly.


### feedsettings.json file.

This file can be found in 1Password. Look in the Ridestyler vault. It should be placed in the same directory as the binary.


### Access to GitHub packages

To gain access to Github packages, go to GitHub.com and login.  Take the following steps:

1. Click on your avatar in the upper-right corner and click "Settings"
2. Click on "Developer Settings"
3. Click on "Personal Access Tokens"
4. Generate new token.  Make sure the token has access to "read:packages"
5. Take note of the token.

6. In Visual Studio, navigate to Tools -> NuGet Package Manager -> Package Manager Settings
7. Click on "Package Sources" and click on the green plus sign
8. Name: Burkson Nuget / Source: `https://nuget.pkg.github.com/burkson/index.json`
9. Click OK

Build the project, or open the NuGet explorer and select the Package Source as either "Burkson Nuget" or "All"

When prompted for credentials, enter your Github ID as the username, and your personal token as the password.  
This provides access within Visual Studio to the Burkson NuGet respository on GitHub.

Once this is set up, you should be able to access all Burkson/Ridestyler nuget packages hosted in GitHub.  For specific instructions on how to publish a package to Github, see the [ridestyler.core.feed](https://github.com/Burkson/ridestyler.core.feed) project.


### feedsettings.json Debug setup

Debug mode disabled the requests to the Ridestyler API for Feed Settings and Response

1. Update feedsettings.json and add "Debug": "true" to the root element
2. To override or simulate settings returned from the Ridestyler API add Debug/PayloadSettings.json to the binary directory (bin/Debug/netcoreapp3.1/)
3. On successful run all files that would be sent to the Riderstyler API will be written to Debug in the binary's directory