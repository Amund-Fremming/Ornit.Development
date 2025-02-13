import { AuthRequest } from "../contenttypes";

	const authenticate = async (token: string) => {
		try {
			const response = await fetch(`api/Auth/authenticate}`, {
				method: "POST",
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
	const refreshToken = async (refreshToken: string) => {
		try {
			const response = await fetch(`api/Auth/refresh-token}`, {
				method: "POST",
				headers: {
					"Content-Type": "application/json",
					
				},
				body: JSON.stringify(refreshToken),
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
	const login = async (request: AuthRequest) => {
		try {
			const response = await fetch(`api/Auth/login}`, {
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
	const register = async (request: AuthRequest) => {
		try {
			const response = await fetch(`api/Auth/register}`, {
				method: "POST",
				headers: {
					"Content-Type": "application/json",
					
				},
				body: JSON.stringify(request),
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
