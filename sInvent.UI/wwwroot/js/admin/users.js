var app = new Vue({
    el: '#app',
    data: {
        username: ""
    },
    mounted() {
    /* todo: get all users */
    },
    methods: {
        createUser() {
            this.loading = true;
            axios.post('/users', { username: this.username })
                .then(res => {
                    console.log(res);
                })
                .catch(error => {
                    console.log(error)
                })
        }   
    }
})