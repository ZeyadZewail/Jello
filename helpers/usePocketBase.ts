"use client";

import { useEffect, useState } from "react";
import PocketBase, { BaseAuthStore } from "pocketbase";
import { useRouter, usePathname } from "next/navigation";

const usePocketBase = () => {
	const pathname = usePathname();
	const { push } = useRouter();
	const pb = new PocketBase("http://127.0.0.1:8090");
	const [authStore, setAuthStore] = useState<BaseAuthStore>();

	useEffect(() => {
		const authenticate = async () => {
			if (pb?.authStore.isValid) {
				await pb.collection("users").authRefresh();
			} else {
				push("/login");
			}

			setAuthStore(pb.authStore);
			if (pathname === "/login") {
				return;
			}
		};

		authenticate();
	}, []);

	return { pb, authStore };
};

export default usePocketBase;
