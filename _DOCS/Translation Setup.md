##### Translations use the BROWSER'S LANGUAGE FROM THE USER SETTINGS.
##### Translation is done through Microsoft's Localization library.

## LANGUAGE FILES
Translations are stored inside the Resources folder of the project;
the filenames' format for the translations is
	App.<lang>.resx
where <lang> is a 2 characters long standard identifier of the language (e.g.: en = English; it = Italian; es = Spanish etc.)
for more specific localization one could use the <lang>.<location> format, like en-US or en-EN.

The format for each resource is:
| Field | Description |
| --- | --- |
| NAME | The ENGLISH word or phrase |
| VALUE | The translation of the word or phrase |
	
e.g. in App.it.resx:
	NAME: Hello
	VALUE: Ciao

## TRANSLATION 
The IStringLocalizer<App> object is instantiated through dependency injection, so it can be accessed through the [Inject] attribute.
This is already setup inside BaseComponent.cs, so always remember to inherit it when required.
To translate from a Razor component, simply invoke the @Translate(string text) method, and write the text in english.

## TESTING
To test a certain language, instead of changing your browser's settings, you can rename the resource files temporarily.
For example: if I want to test the italian translations, I can swap the names of App.resx and App.it.resx, and the 
browser (which is set to English) will recognize the new App.resx as the base translation resource.
