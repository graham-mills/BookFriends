const PageState = {
    DISPLAY_PAGED_RESULTS: 0,
    DISPLAY_SEARCH_RESULTS: 1,
    FETCHING_DATA: 2
}

const page = {
    state: PageState.DISPLAY_PAGED_RESULTS,
    observers: [],

    changeState: function (newState) {
        this.state = newState;
        this.observers.forEach(o => o.onPageStateChanged(newState));
    },
    subscribeToStateChange: function (observer) {
        this.observers.push(observer);
    }
}

const listingsVm = Vue.createApp({
    data() {
        return {
            listings: [],
            totalListings: 0,
            listingsPage: 1,
            listingsPerPage: 0,
            listingsSummary: "",
            displayLoadingSpinner: false
        };
    },
    methods: {
        onPageStateChanged(pageState) {
            this.displayLoadingSpinner = pageState === PageState.FETCHING_DATA;
        },
        updatePagedListings(listings) {
            listings.forEach(l => this.listings.push(l));
            this.listingsPage += 1;
            nextPageButtonVm.displayButton = this.listings.length < this.totalListings;
        },
        updateSearchListings(listings) {
            this.clearListings();
            listings.forEach(l => this.listings.push(l));
            nextPageButtonVm.displayButton = false;
        },
        updateListingsSummary() {
            this.listingsSummary = `Displaying ${this.listings.length} of ${this.totalListings} community groups`;
        },
        clearListings() {
            this.listings = [];
            this.listingsPage = 1;
        }
    },
    beforeUpdate: function () {
        this.updateListingsSummary();
    },
    mounted: function () {
        page.subscribeToStateChange(this);
    }
}).mount('#browse-communities-list');

const nextPageButtonVm = Vue.createApp({
    data() {
        return {
            displayButton: true
        };
    },
    methods: {
        onPageStateChanged(pageState) {
            this.displayButton = pageState != PageState.FETCHING_DATA;
        },
        onClick() {
            this.fetchNextPage();
        },
        fetchNextPage() {
            fetch(`/api/communitygroup/?items=${listingsVm.listingsPerPage}&page=${listingsVm.listingsPage + 1}`, {
                method: "GET"
            }).then(response => this.handleFetchNextPageResponse(response))
                .then(data => this.handleFetchNextPageData(data));
            page.changeState(PageState.FETCHING_DATA);
        },
        handleFetchNextPageResponse(response) {
            page.changeState(PageState.DISPLAY_PAGED_RESULTS);
            return response.json();
        },
        handleFetchNextPageData(data) {
            listingsVm.updatePagedListings(data);
        }
    },
    mounted: function () {
        page.subscribeToStateChange(this);
    }
}).mount("#browse-communities-next-page-button"); 


const searchInputVm = Vue.createApp({
    data() {
        return {
            searchQuery: "",
            searchEnactTimeoutId: null,
            searchEnactTimeoutPeriod: 500
        };
    },
    methods: {
        onSearchInputChanged(event) {
            if (this.searchEnactTimeoutId != null) {
                clearTimeout(this.searchEnactTimeoutId);
            }
            this.searchEnactTimeoutId = setTimeout(this.fetchSearchResults, this.searchEnactTimeoutPeriod);
        },
        fetchSearchResults() {
            listingsVm.clearListings();

            let trimmedQuery = $.trim(this.searchQuery);
            if (trimmedQuery.length > 0) {
                fetch(`/communities/search?searchQuery=${this.searchQuery}`, {
                    method: "GET"
                }).then(response => this.handleSearchQueryResponse(response))
                    .then(data => this.handleSearchQueryData(data));
                page.changeState(PageState.FETCHING_DATA);
            }
            else {
                listingsVm.listingsPage = 0;
                nextPageButtonVm.fetchNextPage();
            }
        },
        handleSearchQueryResponse(response) {
            page.changeState(PageState.DISPLAY_SEARCH_RESULTS);
            return response.json();
        },
        handleSearchQueryData(data) {
            listingsVm.updateSearchListings(data);
        }
    }
}).mount("#browse-communities-search-input");

