import { TestEnum } from "../contenttypes";

	const deletee = async (bodyString: string, testEnum: TestEnum) => {
		try {
			const response = await fetch(`api/test/{prm}}`, {
				method: "DELETE",
				headers: {
					"Content-Type": "application/json",
					
				},
				body: JSON.stringify(bodyString),
			});

			if (!response.ok) {
				const errorMessage = await response.json();
				throw new Error(errorMessage);
			}

			const data = await response.json();
			return data;
		} catch (error) {
		    
		}
	};
	const patch = async (str: string, token: string) => {
		try {
			const response = await fetch(`api/test/patch/${str}}`, {
				method: "PATCH",
				headers: {
					"Content-Type": "application/json",
					Authorization: `Bearer ${token}`
				},
				
			});

			if (!response.ok) {
				const errorMessage = await response.json();
				throw new Error(errorMessage);
			}

			const data = await response.json();
			return data;
		} catch (error) {
		    
		}
	};
	const get = async () => {
		try {
			const response = await fetch(`api/test/get/extra/param}`, {
				method: "GET",
				headers: {
					"Content-Type": "application/json",
					
				},
				
			});

			if (!response.ok) {
				const errorMessage = await response.json();
				throw new Error(errorMessage);
			}

			const data = await response.json();
			return data;
		} catch (error) {
		    
		}
	};
