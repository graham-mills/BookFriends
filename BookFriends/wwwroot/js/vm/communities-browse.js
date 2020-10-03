
let data = {
    communityListings: [],
    totalCommunities: 0,
    displayViewMoreButton: true,
    displayLoadingSpinner: false
};

const vm = Vue.createApp({
    data() {
        return data;
    },
    methods: {
        loadMoreListings(event) {
            console.info("button clicked");
        }
    },
    mounted() { console.info("vm mounted") },
    beforeUpdate() { console.info("vm beforeUpdate")},
    updated() { console.info("vm updated") }
}).mount("#browse-communities");