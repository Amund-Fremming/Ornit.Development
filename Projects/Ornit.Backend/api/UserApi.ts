import { Auth0LoginResponse } from "../contenttypes";

	const get = async (loginreq: Set<Auth0LoginResponse>) => {
		try {
			const response = await fetch(`api/User/`, {
				method: "GET",
				headers: {
					"Content-Type": "application/json",
					
				}
			});

			if (!response.ok) {
				const errorMessage = await response.json();
				throw new Error(errorMessage);
			}

			const data = await response.json();
			return data;
		} catch (error) {
			console.log("Get error: " + error.message);
		}
	};
