# Name: RegisterNugetToOfflinePackages.App
# Purpose: 
- Console app that writes nupkg files and adds to VS Offline Packages folder for offline use.

# Use: 
- When built: Run the exe file with a folder path (it can be a parent level folder, if desired) argument:
  - Example: RegisterNugetToOfflinePackages.App C:\Temp
- When Debugging: Add your folder path argument in Project Properties => Debug => Arguments, then begin debugging

# Note: 
- app.config contains appSettings values that contain the NuGet.Exe path and NugetSource folders; these can be changed to suit your needs.  
  - If you run into any issues using non-Packages X86 foldrs, first try removing the extension method "AddQuotesAroundString()"
  
# If making Code Changes:
  - Please fork this repository, then create a PR to insert fork to this repo
  - When making pull requests, please add me to the reviewers so that I get a notification to take a look at it
  
This repo will have more funcationality built out as I go along for my needs, but please feel free to play around with it and find uses that work for you!
