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

Commiting is the "Meat and Potatoes" of Git. When you create a commit, it's
basically a "save" or "snapshot" of everything you've done up to that point.
Commiting is the equivalent of saving in a videogame. Just like in a 
videogame, it's a really good idea to commit often so if you run into trouble
you can go back to an old save.

Commiting your code always requires a message. This should be a short 
description of what you did, probably less than 50 characters. It doesn't
have to be very detailed, but at the very least it should give you some idea
of what you had done up to that point. Please don't just keyboard-bash your
commit messages, because I guarantee that eventually you'll regret it.

Sometimes, when you commit you might get something called a `merge conflict`.
These are the bane of every programmer's existence, and if you get one and
don't know how to fix it, make sure you get help ASAP. There is a merge
conflict resolution tool in GitHub Desktop, but Unity has its own tool 
designed specifically for merging scenes together. This is native in Git for
Unity, but if you're using normal git, you might want to get some help.

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

Branches allow us to work on different versions of the project at the same
time. Normally, if two people work on the same file and try to commit it,
the server would tell them they have a merge conflict every time they tried
to pull/push. However, creating a branch takes a snapshot of the entire
project at that moment and allows each person to edit them without worrying
about merging.

**We will be making heavy use of branches during this project.**

Every time you have to do something, you should make a new branch for it. This
will make it much easier for everyone involved to work at the same time. There
is also a known problem in Unity where editing a scene can cause a merge 
conflict. **Creating a branch will prevent this as long as you're working on
that branch.** You can also have multiple branches open at once, or create
more branches from your branches if you really wnt to get fancy.

### Pull Requests

While the name may be similar, a pull request is not pulling from the 
repository. Instead, a pull request is how you take all the work you've made
on a branch and put it into the main branch. Whenever you complete something
you're working on, you should submit a pull request.

**The main branch of the project, `master`, is locked.**

This means that you cannot push anything directly to it. Instead, you will
need to create a new branch and submit a pull request to put any code into
the final version of the game. I know this is annoying, but it will prevent
anyone from fucking up our main version of the game. **When you submit a
pull request to `master` you will need one other person to OK it.** This can
be anyone, and is designed so that at least one other person has reviewed
your submission before it's added to the project so that we don't make
mistakes.

## Merge Conflicts

Merging takes the changes made from one branch, and tries to combine them
with those of another branch. Sometimes the computer can't do this
automatically. This generates a merge conflict.

Merge conflicts happen when the GitHub server has a modified version of the
same file that you do. To prevent most merge conflicts, we will each be 
operating on separate branches. However, there may be some cases where you'll
encounter them (like if you're working on the same branch from separate 
machines).

If you have a merge conflict, GitHub desktop should walk you through each
conflict and ask you if you want to keep the local version or the remote
version. In each case, just choose which you want to keep. In the 
worst-case scenario, GitHub will not be able to resolve these merge conflicts.
**There is a known issue with Unity Scenes that causes unresolvable merge
conflicts. This will be covered later.**

If you have a merge conflict and don't know how to resolve it, **Please do 
not guess blindly.** Instead, abort the merge and switch to a different 
branch until you can find someone to help you. If the situation is really
urgent, **submit a pull request to master, and someone will merge your
branch for you.**

### Unity Scenes

Editing a Unity scene can be a giant pain when it comes to working with git.
This is why I recommend working on the same scene in a single branch, and when
you want to merge that scene with master, submit a pull request. I will also
release files on Discord which will add this file automatically.

Another tactic to avoid scene merge conflicts is to notify everyone else
before you start working on a scene. This will help us to know which scenes
we can modify at any given time.

**Remember: if you have a merge conflict and you can't figure it out, someone
will help you. Ideally this won't happen until we go to merge branches, in
which case someone else can perform the merge.**

## Issues

Issues are our way of figuring out what needs to get done and telling each
other what needs to get done. An issue can be opened about literally anything,
and the GitHub issues window lets us be very specific about different parts
of the project. We can even link to specific files or places in the code if
we want to be as specific as possible.

We can assign issues, label issues, and create new issues all from the 
GitHub website. This will be our preferred way of leaving comments on the
project because issues are much more visible than Discord comments.

Anything at all can be an issue, but for more information on how to create
and managing issues, please see `CONTRIBUTING.md`.

## Projects