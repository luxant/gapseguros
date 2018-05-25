new Vue({
	el: '#assigment-component',
	data: {
		userFilter: "",
		selectedUser: null,
		users: [],
		policies: [],
		showSearchTable: false,
		userSearchDebounceTimerId: 0
	},
	computed: {
		isUserSelected: function () {
			return this.selectedUser ? true : false;
		}
	},
	methods: {
		setAuthorizationHeader: function (xhr) {
			xhr.setRequestHeader("Authorization", "Basic " + btoa("my_username" + ":" + "my_password"));
		},
		makeSelectedUser: function (user, event) {
			this.selectedUser = user;
			this.showSearchTable = false;
			this.userFilter = "";
		},
		assignPolicy: function (policy, event) {
			var self = this;
			
			$.ajax({
				url: "api/assignments/assignPolicyToUser/" + self.selectedUser.userId + "/" + policy.policyId,
				method: "post",
				beforeSend: self.setAuthorizationHeader,
				success: function (result) {
					result.data.policy = policy;

					self.selectedUser.policyByUser.push(result.data);
				}
			});
		},
		removePolicyForSelectedUser: function (policyByUser, index, event) {
			var self = this;

			$.ajax({
				url: "api/assignments/unassignPolicyToUser/" + policyByUser.policyByUserId,
				method: "delete",
				beforeSend: self.setAuthorizationHeader,
				success: function (result) {
					self.selectedUser.policyByUser.splice(index, 1);
				}
			});
		}
	},
	watch: {
		userFilter: function (newValue, oldValue) {
			var self = this;

			this.showSearchTable = self.userFilter.length > 0;

			// We debounce the service call to optimize number of requests to the server
			clearTimeout(self.userSearchDebounceTimerId);

			self.userSearchDebounceTimerId = setTimeout(function () {
				$.ajax({
					url: "api/users/getAll/" + self.userFilter,
					beforeSend: self.setAuthorizationHeader,
					success: function (result) {
						self.users = result;
					}
				});
			}, 350);
		}
	},
	mounted: function () {
		var self = this;

		$.ajax({
			url: "api/policies/getAll",
			beforeSend: self.setAuthorizationHeader,
			success: function (result) {
				self.policies = result;
			}
		});
	}
})