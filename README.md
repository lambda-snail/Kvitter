
[![Build status](https://dev.azure.com/blombergniclas/Flow/_apis/build/status/Flow-CI)](https://dev.azure.com/blombergniclas/Flow/_build/latest?definitionId=1)

# Flow

A light-weight social media platform. The platform is built with these purposes in mind:

    1. To learn how to work with Blazor in asp.net.
    2. To learn how to work with MongoDb.
    3. To get more experience with planning and developing an application from scratch.
    4. To practice clean architecture (again).

Another major reason for the project is to try out some lessons that I learned from my previous project, most notably to separate the Identity database from all the other data in the system -- this is expected to reduce pain by a lot.


## Libraries

The platform uses MediatR for CQRS and AutoMapper to facilitate mapping between objects in different layers. For testing the libraries xUnit, Moq and mongo2go are used.

  
## Roadmap

The project just started, so there is a lot of work to be done. Some features that I aim to implement are (not in order):

- Connecting with other users
- Viewing the feeds of friends ("friends feed")
- Feed filter
- Recursive liking of posts: Like a post, like that someone likes the post, like that someone likes that someone ...
- "Mentions" in posts
- Notifications

### Implemented

- User registration/login
    + Users can edit basic details of their profile.
    + Users can upload a profile picture.

- Posting content to the feed
    + The page is refreshed when the user creates a post.
    + Posts link to profile details page.

- Viewing the feeds of other users ("world feed")
    + Only posts that are actually visible on the screen are fetched from DB and rendered.

### If I have more time:

- Instant messaging with friends
- Blocking other users



## License

The code is released under the [MIT](https://choosealicense.com/licenses/mit/) license.
