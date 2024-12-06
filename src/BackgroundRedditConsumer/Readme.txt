This BackgroundRedditConsumer project is the one that periodically invokes the AddPostsWithMostVotesCommandHandler
via Mediatr.

BackgroundRedditConsumer project is also configured to expose a minimal API endpoint to consume the data stored in memory
While RedditHostedService runs in the background, we can still invoke the "/realtime/postswithmostvotes" endpoint
located in Program.cs. This endpoint gets the last set of posts with most votes from the in memory store (ConcurrentDictionary)
You can see on each invocation how the created date changes

appsettings.Development.json has a "RedditAuth" json object with a clientId and secret.
Secret is empty there.

For development purposes I use "User secrets" and have a secrets.json created, which is not part of source control.
Token to use as secret's value: "secret": "2dzTDwbc4GIPn95R2rmnUwwY2cLIuA"
