#Simple.Data.Elasticsearch#

Simple.Data.Elasticsearch is an adapter that allows you to use elasticsearch as a backend with Simple.Data.
**WARNING:** You are looking at a very early version of Simple.Data.Elasticsearch. So far, this is only a **proof of concept**. It does not yet support all features of Simple.Data, and it neither supports all features of elasticsearch. Don't expect to be able to use it in a real project.

##Connecting to an elasticsearch cluster##

To connect to an elasticsearch cluster, you have to use a database opener.

```C#
dynamic db = Database.Opener.OpenElasticsearch("localhost", 9200, "testindex");
```

You can now use the "db" object to interact with elasticsearch. You can re-use this object.

##Indexes and Types##

Simple.Data.Elasticsearch always uses the index you specified when opening the connection. In the examples below, this will always be "testindex" because we passed this index name to the Database.Opener above.

Simple.Data.Elasticsearch tries to find out the type name based on the properties you access in the DB object. When you call methods on the following property, it will perform operations on the type "testindex/products" in elasticsearch:

```C#
db.Products
```

Note that you do not have to declare "Products". Because the "db" object is a "dynamic", you can access any property name you like. We can find out which type you meant based on the name of the property you accessed.

##Finding Documents##

Simple.Data.Elasticsearch creates different kinds of queries for finding documents. When you only search in a single field of a document, it will create a "match_phrase" query. When you search in all fields of a document, it will create a "search" query. I will add a fine-grained way to control which query is created later.

###Get - Retrieve Documents by Their Key###

To retrieve a document by its key, use the "Get"-Method. For example, the following code will try to retrieve "testindex/products/1" from elasticsearch:

```C#
var product = db.Products.Get(1);
```

And this line will try to retrieve "testindex/manufacturers/2":

```C#
var manufacturer = db.Manufacturers.Get(2);
```

###FindAllBy - Searching for documents###

To search for a document, use the Simple.Data "FindAllBy" method:

```C#
List<Product> products = db.Products.FindAllBy(Name: "ACME");
List<Product> products = db.Products.FindAllBy(_: "ACME");
```

The first query searches for all products that contain "ACME" in their name field, the second query searches for all products that contain "ACME" in any field. The first one will be translated to an elasticsearch "match_phrase" query, the second one to a "search" query.

You can also search for documents of any type. Again, first query will be translated to an elasticsearch "match_phrase" query, the second one to a "search" query.

```C#
List<Product> products = db.Products.FindAllBy(Name: "ACME");
List<dynamic> documents = db._.FindAllBy(_: "ACME");
```

Not that the second list is a "List<dynamic>": We can not know if we only got products in this case, so we can not cast the resulting documents to "Product" objects.

##Developers##

Before you run the unit tests, make sure that elasticsearch is running on "localhost:9200".

If you have any questions or feature requests, contact me:
* [business@davidtanzer.net](mailto:business@davidtanzer.net)
* [@dtanzer](http://twitter.com/dtanzer)

##License##

The MIT License (MIT)

Copyright (c) 2013 David Tanzer

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
