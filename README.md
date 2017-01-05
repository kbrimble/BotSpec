# BotSpec

BotSpec is a BDD-style framework for testing chatbots created with the [Microsoft Bot Framework](https://dev.botframework.com/) inspired by [FluentAssertions](http://www.fluentassertions.com/).

## Pre-requisites

BotSpec tests your bot over the [Direct Line channel](https://docs.botframework.com/en-us/faq/#what-is-the-direct-line-channel).
For BotSpec to be able to communicate with your bot, the Direct Line channel needs to be enabled and you need to have your Direct Line
secret or token to hand.

## Getting started

To start sending messages to your bot and testing the responses just create a new instance of the `Expect` object using your Direct Line
secret or token:

```csharp
var expect = new Expect("my secret or token");
```

### Sending messages

Using Expect you can send messages to your bot:

```csharp
expect.SendMessage("Hello!");
```

### Expecting simple responses

And then test the response:

```csharp
expect.Message().TextMatching("Hi!");
```

### Expecting responses that match a pattern

Methods like `TextMatching` take a regex string so can match on patterns:

```csharp
expect.SendMessage("Hello, I am Kristian");
expect.Message().TextMatching("Hi [A-Za-z]*!");
```

The result of a pattern can also be saved for later use:

```csharp
var names = new List<string>();
expect.SendMessage("Hello, I am Kristian");
expect.Message().TextMatching("Hi [A-Za-z]*!", "Hi ([A-Za-z]*)!", out names);
```

The second regex contains [capture groups](https://msdn.microsoft.com/en-us/library/bs2twtah(v=vs.110).aspx#matched_subexpression),
results of which are output as a list of strings (the 3rd parameter).
The `names` list in the above example would contain just one value; "Kristian".

### Expecting attachments

Bots can return a number of different types of [attachments](https://docs.botframework.com/en-us/csharp/builder/sdkreference/attachments.html) which can also be tested:

```csharp
expect.SendMessage("Help me pick a movie");
expect.Message().TextMatching("Choose some movies from this list that you like");
expect.Message().WithAttachment().OfTypeThumbnailCard().TextMatching("Fight Club").WithButtons().TitleMatching("Yes");
expect.Message().WithAttachment().OfTypeThumbnailCard().TextMatching("The Matrix").WithButtons().TitleMatching("Yes");
```

The above code does the following:

1. Sends a message to the bot with the text "Help me pick a movie"
1. Expects a response from the bot with text that matches "Choose some movies from this list that you like"
1. Expects a response from the bot that contains a thumbnail card with text that matches "Fight Club" *and* has a button with a title matching "Yes"
1. Expects a response from the bot that contains a thumbnail card with text that matches "The Matrix" *and* has a button with a title matching "Yes"

The test will pass if ***any*** message matches these conditions. This means that there may be one message that satisifies the expectations shown above or there may be many where different messages satisfy one of the expectations each.

### Putting it all together in a test

Using NUnit a test putting all of that together may look like:

```csharp
[Test]
public void When_I_ask_for_help_picking_a_movie_I_get_choices_on_buttons_back()
{
    var expect = new Expect("my token here");
    expect.SendMessage("Hello, I am Kristian");
    expect.Message().TextMatching("Hi [A-Za-z]*!");
    expect.SendMessage("Help me pick a movie");
    expect.Message().TextMatching("Choose some movies from this list that you like");
    expect.Message().WithAttachment().OfTypeThumbnailCard().TextMatching("Fight Club").WithButtons().TitleMatching("Yes");
    expect.Message().WithAttachment().OfTypeThumbnailCard().TextMatching("The Matrix").WithButtons().TitleMatching("Yes");
}
```
