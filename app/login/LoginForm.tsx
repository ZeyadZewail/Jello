"use client";

import Spinner from "../../components/spinner/Spinner";
import { useEffect, useRef, useState } from "react";
import { useRouter } from "next/navigation";

const LoginForm = () => {
	const formRef = useRef(null);
	const [form, setForm] = useState({ email: "", password: "" });
	const [error, setError] = useState("");
	const [loading, setLoading] = useState(false);
	const { push } = useRouter();

	useEffect(() => {
		const attemptLogin = async () => {
			// if (pb?.authStore.isValid) {
			// 	push("/home");
			// }
		};

		attemptLogin();
	}, []);

	const handleChange = (e: React.FormEvent<HTMLInputElement>) => {
		const target = e.target as HTMLInputElement;
		const { name, value } = target;

		setForm({ ...form, [name]: value });
	};

	const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
		e.preventDefault();
		if (loading) {
			return;
		}
		setLoading(true);
		try {
			// const authData = await pb.collection("users").authWithPassword(form.email, form.password);
			push("/home");
		} catch (error) {
			setError("Wrong Email or Password");
			setLoading(false);
		}
	};

	return (
		<div className="flex flex-col shadow-md p-6 gap-2 bg-opacity bg-primary bg-opacity-5 rounded-md">
			<h2 className="text-4xl text-accent text-center">Kanban Z</h2>
			{error ? <p className="text-center text-red-200 px-6 py-2 bg-red-500 rounded-lg">{error}</p> : null}
			<form ref={formRef} onSubmit={handleSubmit} className="flex flex-col gap-4">
				<label className="flex flex-col">
					<span className="font-medium mb-2">Email</span>
					<input
						type="email"
						name="email"
						value={form.email}
						onChange={handleChange}
						placeholder="Email"
						required
						className="py-2 px-6 text-secondary rounded-lg outlined-none border-none font-medium"
						autoComplete="hidden"
					/>
				</label>
				<label className="flex flex-col">
					<span className="font-medium mb-2">Password</span>
					<input
						type="password"
						name="password"
						value={form.password}
						onChange={handleChange}
						required
						placeholder="Password"
						className=" py-2 px-6 text-secondary rounded-lg outlined-none border-none font-medium"
						autoComplete="hidden"
					/>
				</label>
				<button
					type="submit"
					className="py-3 w-24 justify-center flex px-8 min-w-30 bg-primary-button font-bold rounded-xl self-center hover:bg-opacity-80">
					{loading ? <Spinner size={24} /> : "Login"}
				</button>
			</form>
		</div>
	);
};

export default LoginForm;
