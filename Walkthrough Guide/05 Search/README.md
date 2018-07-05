![Banner](Assets/Banner.png)

# Azure Search
Many applications use search as the primary interaction pattern for their customers who expect great relevance, suggestions, near-instantaneous responses, multiple languages, faceting and more. Azure Search makes it easy for you to add powerful and sophisticated search capabilities to your website or application. The integrated Microsoft natural language stack, also used in Bing and Office, has been improved over 16 years of development. Quickly tune search results and construct rich, fine-tuned ranking models to tie search results to business goals. Reliable throughput and storage give you fast search indexing and querying to support time-sensitive search scenarios.

We're going to add Azure Search to our project for searching the jobs. Right now we're using it in the most basic possible way, but we'll be expanding this later as the app grows in complexity. 

## Deploying Azure Search 

![Search for Azure Search](Assets/SearchForSearch.png)
Click the "Create a Resource" in the top-left menu bar. You'll then be able to search for 'Azure Search'. 

![Azure Search Results](Assets/SearchResults.png)
Select Azure Search and click 'Create'. 


![Azure Search Configure](Assets/ConfigureSearchService.png)
You'll have a few options for pricing, but for this demo, we should have plenty of capacity left over if we use the Free tier. Once you've deployed Azure Search, go to the resource 

![Azure Search Overview](Assets/SearchOverview.png)

### Indexing our data
There are two ways to get data into Azure Search. The easiest is to make use of the automatic indexers. With the indexers, we're able to point Azure Search to our database and have it on a schedule look for new data. This can lead to situations where the database and search index are out-of-sync so be wary of using this approach in production. Instead, you should manage the search index manually using the lovely SDKs provided. 

For ease of use, we'll make use of the Indexers to get some data quickly into our index. 

To do this, click the "Import Data" button in the top toolbar. 

![Azure Search Import Data](Assets/ImportData.png)

![Azure Search Connect To Data](Assets/ConnectToDataDefault.png)
We already have our database deployed, so we can select "Cosmos DB" and then click "Select an account". 

![Azure Search New Data Source](Assets/NewDataSourceFilledIn.png)
Once you've selected your Cosmos DB account, you should be able to use the drop-downs to select which database and collections you wish to import from. We'll be picking "Jobs". 

![Azure Search Create Index](Assets/CreatingJobsIndex.png)

![Azure Search Create Index](Assets/CreateJobIndexBasic.png)
**Important Note**
The Index name must be set to "job-index", because it is referred to by name in the mobile application.

We need to configure what data we wish to send back down to the device with a search query as well as which properties we'll use to search. The Index is difficult to modify (apart from adding new fields) after we've created it, so its always worth double checking the values.

**Important**
You need to create a _suggester_ called 'suggestions'. This is referred to by the _search_ API which we're writing. To do this, tick the 'suggester' box and enter 'suggestions' as its name. Then you also need to mark at least one field as being part of the suggester. We suggest(!) that the _Name_ and _Details_ fields are marked as such.

Note that the screenshot above is slightly out of date, and the _Suggester_ is now presented as a checkbox on the main screen, rather than another tab. Also note that at the moment the Suggester details aren't visible in the index once you've created (this is a shortcoming of the current Azure portal).

Once you've completed this setup, click "Create". 

![Azure Search Create Updates](Assets/IndexerSchedule.png)
You can now set the frequenancy at which Azure Search will look for new data. I recommend for this demo setting it to be 5 minutes. We can do this by selecting "custom". 

![Azure Search Customer Timer](Assets/CustomTimer.png)
We also want to track deletions, so go ahead and check the tickbox and select the 'isDelete' item from the drop-down menu and set the marker value to "true". 

You're now ready to click "OK" which will create the indexer and importer for you. 

![Azure Search Indexers List](Assets/Indexers.png)
Click on the "indexers" item within the overview blade. 

![Azure Search Run Indexer](Assets/RunIndex.png)
We can now run the indexer for the first time. Once its completed, navigate back to the Azure Search Overview and click on "Search Explorer". We can now confirm if the search service is working as expected. 

![Azure Search Explorer](Assets/SearchExplorer.png)

# Next Steps 
[API Management](../07%20API%20Management)



