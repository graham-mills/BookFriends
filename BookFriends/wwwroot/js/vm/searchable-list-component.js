let searchableListComponent = {
    data() {
        return {
            pagesStored: [],
            listItems: [],
            displayedListItems: [],
            totalListPages: 0,
            totalListItems: 0,
            listPage: 1,
            listPageSize: 0,
            statusText: "",
            displayLoadingSpinner: false,
            searchQuery: "",
            searchEnactTimeoutId: null,
            searchEnactTimeoutPeriod: 500,
            requestedListPage: 0,
            apiUrl: "",
            endlessScrolling: false
        };
    },
    methods: {
        // Listeners
        onSearchInputChanged() {
            if (this.searchEnactTimeoutId != null) {
                clearTimeout(this.searchEnactTimeoutId);
            }
            this.searchEnactTimeoutId = setTimeout(this.onSearchEnactTimeout, this.searchEnactTimeoutPeriod);
        },
        onSearchEnactTimeout() {
            this.fetchSearchResults(1);
        },
        onNextPageClick() {
            const requestedPage = this.listPage + 1;
            if (this.pagesStored.includes(requestedPage)) {
                this.changePage(requestedPage);
            }
            else if (this.isSearchQueryEmpty()) {
                this.fetchPage(requestedPage);
            }
            else {
                this.fetchSearchResults(requestedPage);
            }
        },
        onPrevPageClick() {
            const requestedPage = this.listPage - 1;
            if (this.pagesStored.includes(requestedPage)) {
                this.changePage(requestedPage);
            }
            else if (this.isSearchQueryEmpty()) {
                this.fetchPage(requestedPage);
            }
            else {
                this.fetchSearchResults(requestedPage);
            }
        },
        onGetPageClick(pageNumber) {
            const requestedPage = pageNumber;
            if (this.pagesStored.includes(requestedPage)) {
                this.changePage(requestedPage);
            }
            else if (this.isSearchQueryEmpty()) {
                this.fetchPage(requestedPage);
            }
            else {
                this.fetchSearchResults(requestedPage);
            }
        },
        // API
        fetchData(url, responseHandler, dataHandler) {
            fetch(url, {
                method: "GET"
            }).then(response => responseHandler(response))
                .then(data => dataHandler(data));
            this.onFetchingData();
        },
        fetchPage(pageNumber) {
            if (pageNumber < 1|| pageNumber > this.totalListPages)
                return;

            this.requestedListPage = pageNumber;
            const offset = (pageNumber - 1) * this.listPageSize;

            this.fetchData(`${this.apiUrl}limit=${this.listPageSize}&offset=${offset}`,
                this.handleFetchPageResponse,
                this.handleFetchPageData);
        },
        fetchSearchResults(pageNumber) {
            this.searchEnactTimeoutId = null;
            if (!this.isSearchQueryEmpty()) {
                this.requestedListPage = pageNumber;

                const offset = (pageNumber - 1) * this.listPageSize;

                this.fetchData(`${this.apiUrl}q=${this.searchQuery}&limit=${this.listPageSize}&offset=${offset}`,
                    this.handleSearchQueryResponse,
                    this.handleSearchQueryData);

                if (pageNumber === 1) {
                    this.clearListItems();
                }
            }
            else {
                this.clearListItems();
                this.fetchPage(1);
            }
        },
        handleSearchQueryResponse(response) {
            this.onFetchResponse();
            return response.json();
        },
        handleSearchQueryData(data) {
            this.updateListItems(this.requestedListPage, data.data);
            this.totalListItems = data.totalRecords;
            this.totalListPages = Math.ceil(this.totalListItems / this.listPageSize);
            this.changePage(this.requestedListPage);
            if (this.listItems.length === 0) {
                this.statusText = `No results found for "${this.searchQuery}"`;
            }
            else {
                this.statusText = `Displaying results for "${this.searchQuery}"`;
            }
        },
        handleFetchPageResponse(response) {
            this.onFetchResponse();
            return response.json();
        },
        handleFetchPageData(data) {
            this.updateListItems(this.requestedListPage, data.data);
            this.totalListItems = data.totalRecords;
            this.totalListPages = Math.ceil(this.totalListItems / this.listPageSize);
            this.changePage(this.requestedListPage);
        },
        // State changes
        onFetchingData() {
            this.displayLoadingSpinner = true;
            this.statusText = "";
        },
        onFetchResponse() {
            this.displayLoadingSpinner = false;
        },
        // Display
        updateListItems(pageNumber, pageItems) {
            pageItems.forEach(l => this.listItems.push(l));
            this.pagesStored.push(pageNumber);
        },
        clearListItems() {
            this.listItems = [];
            this.displayedListItems = [];
            this.pagesStored = [];
            this.listPage = 1;
            this.totalListPages = 1;
        },
        changePage(pageNumber) {
            this.listPage = pageNumber;

            if (this.endlessScrolling) {
                if (this.displayedListItems.length < this.listItems.length) {
                    for (var i = this.displayedListItems.length; i < this.listItems.length; ++i) {
                        this.displayedListItems.push(this.listItems[i]);
                    }
                }
            }
            else {
                const pageStartIndex = (pageNumber - 1) * this.listPageSize;
                this.displayedListItems = [];
                for (var i = 0; i < this.listPageSize; ++i) {
                    if (pageStartIndex + i < this.listItems.length) {
                        this.displayedListItems.push(this.listItems[pageStartIndex + i]);
                    }
                    else {
                        break;
                    }
                } 
            }
        },
        // Util
        isSearchQueryEmpty() {
            let trimmedQuery = $.trim(this.searchQuery);
            return trimmedQuery.length === 0;
        }
    },
    updated: function () {
    }
};