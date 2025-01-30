import {  } from "../contenttypes";

	const get = async (param: string, srs: string[], token: string) => {
		try {
			const response = await fetch(`api/User/api/${param}}`, {
				method: "GET",
				headers: {
					"Content-Type": "application/json",
					Authorization: `Bearer ${token}`
				},
				
			});

			if (!response.ok) {
				const errorMessage = await response.json();
				throw new Error(errorMessage);
			}

			const data:  = await response.json();
			return data;
		} catch (error) {
		    
		}
	};
