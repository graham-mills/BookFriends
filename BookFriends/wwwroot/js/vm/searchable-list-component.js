let searchableListComponent = {
    data() {
        return {
            listings: [],
            totalListings: 0,
            listingsPage: 1,
            listingsPerPage: 0,
            listingsSummary: "",
            displayLoadingSpinner: false,
            searchQuery: "",
            searchEnactTimeoutId: null,
            searchEnactTimeoutPeriod: 500,
            displayNextPageButton: true
        };
    },
    methods: {
        // Listeners
        onSearchInputChanged(event) {
            if (this.searchEnactTimeoutId != null) {
                clearTimeout(this.searchEnactTimeoutId);
            }
            this.searchEnactTimeoutId = setTimeout(this.fetchSearchResults, this.searchEnactTimeoutPeriod);
        },
        onNextPageClick(event) {
            this.fetchNextPage();
        },
        // API
        fetchData(url, responseHandler, dataHandler) {
            fetch(url, {
                method: "GET"
            }).then(response => responseHandler(response))
                .then(data => dataHandler(data));
            this.onFetchingData();
        },
        fetchNextPage() {
            this.fetchData(`/api/communitygroup/?items=${this.listingsPerPage}&page=${this.listingsPage + 1}`,
                this.handleFetchNextPageResponse,
                this.handleFetchNextPageData);
        },
        fetchSearchResults() {
            this.listings = [];
            this.listingsPage = 1;
            let trimmedQuery = $.trim(this.searchQuery);
            if (trimmedQuery.length > 0) {
                this.fetchData(`/communities/search?searchQuery=${this.searchQuery}`,
                    this.handleSearchQueryResponse,
                    this.handleSearchQueryData);
            }
            else {
                this.listingsPage = 0;
                this.fetchNextPage();
            }
        },
        handleSearchQueryResponse(response) {
            this.onFetchResponse();
            return response.json();
        },
        handleSearchQueryData(data) {
            data.forEach(l => this.listings.push(l));
            this.displayNextPageButton = false;
        },
        handleFetchNextPageResponse(response) {
            this.onFetchResponse();
            return response.json();
        },
        handleFetchNextPageData(data) {
            data.forEach(l => this.listings.push(l));
            this.listingsPage += 1;
            this.displayNextPageButton = this.listings.length < this.totalListings;
        },
        // State changes
        onFetchingData() {
            this.displayNextPageButton = false;
            this.displayLoadingSpinner = true;
            this.listingsSummary = "";
        },
        onFetchResponse() {
            this.displayLoadingSpinner = false;
        },
        updateListingsSummary() {
            this.listingsSummary = `Displaying ${this.listings.length} of ${this.totalListings} community groups`;
        }
    },
    updated: function () {
        this.updateListingsSummary();
    }
};