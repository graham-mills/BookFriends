const PageState = {
    DISPLAY_PAGED_RESULTS: 0,
    DISPLAY_SEARCH_RESULTS: 1,
    FETCHING_DATA: 2
}

const page = {
    state: PageState.DISPLAY_PAGED_RESULTS
}

const listingsVm = Vue.createApp({
    data() {
        return {
            listings: [],
            totalListings: 0,
            listingsPage: 1,
            listingsPerPage: 0,
            listingsSummary: "",
            pageState: page.pageState
        };
    },
    methods: {
        onPageStateChange(pageState) {

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
    computed: {
        displayLoadingSpinner: function () {
            return page.state === PageState.FETCHING_DATA;
        }
    },
    beforeUpdate: function () {
        this.updateListingsSummary();
    }
}).mount('#browse-communities-list');

const nextPageButtonVm = Vue.createApp({
    data() {
        return {
            displayButton: true
        };
    },
    methods: {
        onPageStateChange(pageState) {
        },
        onClick() {
            this.fetchNextPage();
        },
        fetchNextPage() {
            fetch(`/api/communitygroup/?items=${listingsVm.listingsPerPage}&page=${listingsVm.listingsPage + 1}`, {
                method: "GET"
            }).then(response => this.handleFetchNextPageResponse(response))
                .then(data => this.handleFetchNextPageData(data));
            page.state = PageState.FETCHING_DATA;
        },
        handleFetchNextPageResponse(response) {
            page.state = PageState.DISPLAY_PAGED_RESULTS;
            return response.json();
        },
        handleFetchNextPageData(data) {
            listingsVm.updatePagedListings(data);
        }
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
                page.state = PageState.FETCHING_DATA;
            }
            else {
                listingsVm.listingsPage = 0;
                nextPageButtonVm.fetchNextPage();
            }
        },
        handleSearchQueryResponse(response) {
            page.state = PageState.DISPLAY_SEARCH_RESULTS;
            return response.json();
        },
        handleSearchQueryData(data) {
            listingsVm.updateSearchListings(data);
        }
    }
}).mount("#browse-communities-search-input");

