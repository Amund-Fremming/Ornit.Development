import { AuthRequest } from "../contenttypes";

	const authenticate = async (token: string) => {
		try {
			const response = await fetch(`api/Auth/authenticate`, {
				method: "POST",
				headers: {
					"Content-Type": "application/json",
					Authorization: `Bearer ${token}`
				}
			});

			if (!response.ok) {
				const errorMessage = await response.json();
				throw new Error(errorMessage);
			}

			const data = await response.json();
			return data;
		} catch (error) {
			console.log("Authenticate error: " + error.message);
		}
	};
	const refreshToken = async (refreshToken: string) => {
		try {
			const response = await fetch(`api/Auth/refresh-token`, {
				method: "POST",
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
			console.log("RefreshToken error: " + error.message);
		}
	};
	const login = async (request: AuthRequest) => {
		try {
			const response = await fetch(`api/Auth/login`, {
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
			console.log("Login error: " + error.message);
		}
	};
	const register = async (request: AuthRequest) => {
		try {
			const response = await fetch(`api/Auth/register`, {
				method: "POST",
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
			console.log("Register error: " + error.message);
		}
	};
