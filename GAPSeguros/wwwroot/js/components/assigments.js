new Vue({
	el: '#assigment-component',
	data: {
		userFilter: "",
		selectedUser: null,
		users: [],
		assignments: [],
		policies: [],
		showSearchTable: false
	},
	computed: {
		isUserSelected: function () {
			return this.selectedUser ? true : false;
		}
	},
	methods: {
		makeSelectedUser: function (user, event) {
			this.selectedUser = user;
			this.showSearchTable = false;
			this.userFilter = "";
			this.assignments = [];

			// Add users to the assigned list
			//this.selectedUser.policyByUser.forEach(x => this.assignments.push(x.policy));
		},
		assignPolicy: function (policy, event) {
			var self = this;
			
			$.ajax({
				url: "api/assignments/assignPolicyToUser/" + self.selectedUser.userId + "/" + policy.policyId,
				method: "post",
				success: function (result) {
					self.assignments.push(policy);

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

			$.ajax({
				url: "api/users/getAll/" + self.userFilter,
				success: function (result) {
					self.users = result;
				}
			});
		}
	},
	mounted: function () {
		var self = this;

		$.ajax({
			url: "api/policies/getAll",
			success: function (result) {
				self.policies = result;
			}
		});
	}
})