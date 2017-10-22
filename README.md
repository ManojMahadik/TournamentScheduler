# TournamentScheduler
Web application to schedule matches for tournament given number of teams.Following are constraints:
- Accept N number of teams
- Each team must play against every other team once home and away
- Maximum 2 matches per day are allowed
- No team should play on consecutive day

Running the application:
1. Download the source code from git.
2. Rebuild the solution. It will fetch all nuget package dependencies.
3. Set the MatchScheduler as startup project (under Presentation folder)
4. Make sure that localdb ((localdb)\mssqllocaldb) is available on your machine. 
5. If localdb is not available on your machine, change the connection string in Web.config file in MatchScheduler project. Use the SQL server availble on your machine.
6. Start application without debugging. At first the application will create database.
7. You can create a Tournament schedule by filling data in the form presented in home page.
8. When you click on submit button a tournament schedule will be generated with an ID. You can always navigate to home/schedule/{id} link to access any previously generated schedule.
