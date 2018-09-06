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

When submitting a pull request for a branch you've been working on, be sure to
specify in the comments whether you'd like the branch to remain open or be 
deleted should the merge request be approved. If you want to get your branch
up to the latest version of master, submit a merge request and say not to 
delete the branch.

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

### Pushing and Pulling

These aren't rules, more like general guidelines. To avoid merge conflicts as
much as possible, you should generally **pull** before you start any work,
even if you don't think there have been any updates since you last pulled.

Similarly, it's a good idea to **commit** your work every time you finish with
a single task. Once again, see the `File Updates` section of this document.
Also, it's a good idea to push after every commit. This ensures your changes 
are saved if anything happens to your device.

## Issues

### Tag Conventions

When submitting any kind of issue, it can be difficult to determine who should
take the issue on as a task. This is why we will add several different tags to
our issues so that they can be filtered and assigned more easily.

**ALL issues must contain AT LEAST ONE tag from the following categories:**

**Area:** Information about who can fix the bug.

- Programming
- Art
- Audio
- Unity
- Other

**Type:** Information about the request.

- Bug Report
- Help Request
- Feature Suggestion
- Code Red

If you're not sure which tags belong, you can add multiple. If you're still 
having issues tagging it, be sure to say so in the comments.

### Creating a Bug Report

When creating a bug report, you should always include an image of the bug in 
your description. If the bug has no image, be sure to include the problem code
or a link to the file where you think the bug occurs.

As always, try to provide as detailed of a bug report as possible, so that we 
can track and resolve the issue quickly and efficiently. The report needs to 
include:

- The branch you're working on
- A brief description of the problem
- Anything that would help a developer or artist find and resolve the bug

Reports may also include:

- Suggestions for the fix
- Tags to specific artists or developers

The more information we have on the issue, the faster we can fix it. And 
remember: **If you don't submit an issue containing the bug, it might not get
fixed.** So please be sure to submit an issue on github instead of telling 
someone about it.

### Submitting a Help Request

A help request is the fastest way to get another person to take over one or
more of your responsibilities. If you're ever stuck on something or can't
complete it in time, please submit an issue on github so another artist or
developer can get working on it ASAP.

Your help request should include:

- Your current branch
- A general overview of the thing you need help with.
- A description of the problem OR
- Why you can't complete the given task

If you know that you won't be able to finish something on time, try to get an
issue in as soon as possible, because we're all on the same deadlines, and we
need time to be able to help you out. Also, be sure to thank the person who 
takes your request.

Also, if you decide to take a help request, be sure to comment on the issue
to let everyone else know that you're working on it.

### Submitting a Feature Suggestion

There is no right way to submit a feature, but if you have any implementation
ideas or other suggestions, be sure to leave them in the comment section of the
issue.

If you're implementing a feature, you can always ask questions on the idea or
feature using the comments section on the issue. The person who initially 
submitted the feature request should then do their best to answer your
questions.

If you don't think the feature should be implemented, make sure that you say so
in the comments of the issue. Any feature that doesn't get implemented must
have a comment saying that the feature was not implemented.

### Submitting a CODE RED Notification

If you have a problem that you can't resolve, needs urgent attention, or breaks
the current build of the game, you need to submit a Code Red notification. These
issues are not to be taken lightly, and should only be submitted if something
needs the immediate attention of one or more people.

When submitting a request, give as much information as possible, and be sure to
set the tags appropriately. If you're not sure what to include, see the Tag
Conventions, Bug Reporting, or Help Request section of this document.

Whenever you log into github, you need to check and make sure there are no
active Code Red notifications, because these are the ones that require
everyone's immediate attention.

## Projects

## Naming Conventions
