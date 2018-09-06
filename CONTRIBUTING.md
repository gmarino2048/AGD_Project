# Contributing Rules

## Committing

### Communication

**All updates to the master branch will be announced in the `file-updates`
channel on discord.**

If you have changes that need to be made to the master branch, you will need
to submit a pull request. **When you submit a pull request, please announce
it in the `file-updates` channel.**

If you want to make changes to another person's code or assets, be sure to 
let them know. You can create a new branch to modify their code, or you can
submit an issue and tag them in the issue.

### Branch Conventions

Any work you do should be on a separate branch from `master`. Commits
directly to the `master` branch should be reserved for **team leads only**.
This is to prevent confusion as our project gets more and more complex. Even
then, team leads must create an announcement every time the master branch is
updated.

The master branch is locked. This means that it cannot be modified by anyone
without first undergoing a pull request review. **If you have an important
branch that you would like to lock, you should be able to edit the branch
settings yourself.**

If you would like to update a file, you need to create and checkout a new
branch. **All the work that you do on this branch should go towards the same
general goal.** For example, if you want to update the contributing rules you
should checkout a new branch, modify this file, and then submit a pull 
request.

When naming your branch, you should always name the branch as follows:

`LastName_BranchInfo`

Where LastName is your last name and BranchInfo is a very brief (< 20 
characters) description of what that branch is for. This will help you keep
track of your own branches, and also help identify changes in pull requests.

### Multiple Users

In general, it's a bad idea to have 2 people working on the same branch at 
once. This is due to the fact that if someone pushes a file to your branch
that conflicts with your changes to a file, it will cause a merge confilct
that needs to be resolved.

**This is never fun for anyone.**

So, in general, it's best to have one person working on a branch at any given
time. If you want to work with someone, just be sure that you're not
modifying the same files at the same time.

A more advanced solution is to create a new branch off of someone else's. You
can then add your changes and merge your branch with theirs. This will cause
the fewest issues, but is not exactly easy for new git users. If you'd like to
do this but don't know how, feel free to ask one of your other teammates.

### File Updates

In general, **you should only be changing one file per commit**. Commits are
like save points in a game. They're unlimited and the more you do it, the 
harder it is to mess things up. If you do make a mistake, you can undo changes
between commits, but if ALL your changes are in a single commit one mistake
can undo all your work.

I'm not going to add a file limit to each commit because that's stupid, but
I will ask that you use the golden rule:

**If you can't say it in 50 characters or less, it's not a single commit.**

## Issues

## Projects

## Naming Conventions
