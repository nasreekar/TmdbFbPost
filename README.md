# TmdbFbPost
A small exercise to get data from Movie API and post on Fb page

## TestCase    
An event in company MovieTHT, where one of the digital marketer was told to increase the sales of movie tickets by promoting movie on one of their Facebook pages. However, he has no knowledge of coding at all, so he is asking you to help him pulling the information from movie websites and post it to the their Facebook pages.

Since the Facebook pages need to reach the audiences as soon as some new popular movie was released, it needs to be performance optimised (aka. Fast). You will need to write a non blocking function to retrieve latest movie from themoviedb and post it on public Facebook pages.
Specifically, you need to pull the latest movie with synopsis, trailer video url and post in on Facebook pages. It is a plus to apply domain model design pattern while structuring the code.
For web services, the documentation can be found at http://docs.themoviedb.apiary.io (This has been deprecated and will be redirected to new site), and you can use this key to call the API: *API KEY*. For Facebook pages, post it to this page id *PAGE ID*

## Considerations
Latest movie API (https://api.themoviedb.org/3/movie/latest?api_key=api-key&language=en-US) generates one result per call, but the movies are very old (sometimes generating results which are released in 1950's and 60's). So I have used Upcoming movies API (https://api.themoviedb.org/3/movie/upcoming?api_key=api-key&language=en-US) which generates multiple results (all the results from specific time to time). I've used the first value of this list to post of FB page because of which I had to compromise of making this task a scheduled task and look for latest movies. Will update the code if the Latest movie API gets updated.
