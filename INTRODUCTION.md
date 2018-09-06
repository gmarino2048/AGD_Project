# Introduction to Git and Github

### This file will be available for everyone to view the basic in's and out's of working with Git

## General

Git is a type of Version Control System (VCS), which basically means that it
keeps track of everything you do. Then, if we screw something up we can 
revert to the previous version when it actually worked.

For all intents and purposes, Git also works as a sort of cloud system. We
can use it as a place to store all our files like iCloud or Azure. This then
lets us work on the same project at the same time, across multiple computers.
The GitHub server is referred to as the `remote`, so if you ever see this
just remember that it's referring to the content on the server.

GitHub is just a collection of servers that allow this to happen. The site
also gives us several team management functions as well as a nice UI to use
Git (which was originally intended for the command line only).

Throughout the course of the semester we will be using Github to manage the
source code of our project. This includes submitting bug reports and feature
requests, as well as communicating between teammates about more 
project-specific topics within the code itself.

## Committing

## Pull/Push

### Pull

Pulling is how you get everything from GitHub. When you pull, your computer
tells the server to give it everything it doesn't already have. Your computer
then downloads all the files and other version data that it doesn't currently
have.

In general, it's a very good idea to pull every time you get started working,
just to make sure that you have everything before you start working. If you
try to upload your stuff without pulling first, you'll get an error that looks
something like:

`Error: the remote has changes that you do not`

In this case, you need to pull, commit, and then push.

### Push

Pushing is how you upload all of your files to the GitHub server. After you
commit all your files, you can then save them remotely on GitHub by pushing.
In some cases, trying to push may result in the error described in the 
**Pull** section of this description, in which case you need to pull before
trying to push again.

In general, it's a good idea to push to the server after every commit,
because if somthing happens to your computer then all your changes will be 
lost. Think of it as a complicated backup of everything you have at that
moment.

## Branches

## Merge Conflicts

## Issues

## Projects