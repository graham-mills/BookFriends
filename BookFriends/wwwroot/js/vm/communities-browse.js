const vm = Vue.createApp({
    data() {
        return {
            communityListings: [],
            totalCommunities: 0,
            listingsPage: 1,
            listingsPerPage: 0,
            displayViewMoreButton: true,
            displayLoadingSpinner: false
        };
    },
    methods: {
        loadMoreListings(event) {
            fetch(`/api/communitygroup/?items=${this.listingsPerPage}&page=${this.listingsPage + 1}`, {
                method: "GET"
            }).then(response => this.handleFetchResponse(response))
              .then(data => this.handleFetchData(data));
            
            this.displayViewMoreButton = false;
            this.displayLoadingSpinner = true;
        },
        handleFetchResponse(response) {
            this.displayViewMoreButton = true;
            this.displayLoadingSpinner = false;
            return response.json();
        },
        handleFetchData(data) {
            data.forEach(listing => this.communityListings.push(listing));
            this.listingsPage += 1;
            this.displayViewMoreButton = (this.communityListings.length < this.totalCommunities);
        }
    }
}).mount("#browse-communities");

